using ServerAPI.Dependency;
using Ss.Core.RedisCache;
using Ss.Data.Enums;
using Ss.Data.Models;
using Ss.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;

namespace ServerAPI.ActionFilters
{
    public class UserRole
    {
        public int Role_Id { get; set; }
        public string RoleName { get; set; }
        public List<RolePermission> Permissions = new List<RolePermission>();
    }

    public class RolePermission
    {
        public int Permission_Id { get; set; }
        public string PermissionDescription { get; set; }
    }


    public class RbacUser
    {
        public int User_Id { get; set; }
        public bool IsSysAdmin { get; set; }
        public string Username { get; set; }
        private List<UserRole> Roles = new List<UserRole>();

        
        ICacheProvider cacheRedisProvider = UnityConfig.Container.Resolve<ICacheProvider>();
        string extendedKeyCache;
        public RbacUser(string _username)
        {
            extendedKeyCache = _username + "_roleKey";
            this.Username = _username;
            this.IsSysAdmin = false;
            GetDatabaseUserRolesPermissions();

        }

        private void GetDatabaseUserRolesPermissions()
        {
            IRepositoryContext context = UnityConfig.Container.Resolve<IRepositoryContext>();

            bool checkMember = cacheRedisProvider.IsInCache(extendedKeyCache);
            if (!checkMember)
            {
                User _user = context.UserRepository.Get().Where(u => u.UserName == this.Username).FirstOrDefault();
                List<string> listAccessPermissions = new List<string>();
                if (_user != null && _user.Actflg == Actflg.Active)
                {
                    Role isRoleRoot = _user.Roles.FirstOrDefault(r => r.RoleName == "Root");
                    if (isRoleRoot != null)
                    {
                        // Add full permistion for Root Admin
                        UserRole _userRole = new UserRole { Role_Id = isRoleRoot.Id, RoleName = isRoleRoot.RoleName };
                        foreach (AccessPermission role_permission in context.AccessPermissionRepository.Get())
                        {
                            listAccessPermissions.Add(role_permission.AccessPermissionDescription);
                            _userRole.Permissions.Add(new RolePermission { Permission_Id = role_permission.Id, PermissionDescription = role_permission.AccessPermissionDescription });
                        }
                        this.IsSysAdmin = true;
                        this.Roles.Add(_userRole);
                    }
                    else
                    {
                        User_Id = _user.Id;
                        foreach (Role _role in _user.Roles)
                        {
                            UserRole _userRole = new UserRole { Role_Id = _role.Id, RoleName = _role.RoleName };

                            var roleAccessPermissions = context.RoleAccessPermissionRepository.Get().Where(role => role.Role.Id == _role.Id);

                            foreach (RoleAccessPermission rolePermission in roleAccessPermissions)
                            {
                                listAccessPermissions.Add(rolePermission.AccessPermission.AccessPermissionDescription);
                                _userRole.Permissions.Add(new RolePermission { Permission_Id = rolePermission.AccessPermission.Id, PermissionDescription = rolePermission.AccessPermission.AccessPermissionDescription });
                            }

                            this.Roles.Add(_userRole);

                            if (!this.IsSysAdmin)
                            {
                                this.IsSysAdmin = _role.IsSysAdmin;
                            }

                        }
                    }
                }
                cacheRedisProvider.Set(extendedKeyCache, listAccessPermissions);
            }
        }

        public bool HasPermission(string requiredPermission)
        {
            bool checkMember = cacheRedisProvider.IsInCache(extendedKeyCache);

            if (checkMember)
            {
                return (cacheRedisProvider.Get<List<string>>(extendedKeyCache).FirstOrDefault(x => x.ToLower() == requiredPermission.ToLower()) != null);
            }
            else
            {
                bool bFound = false;
                foreach (UserRole role in this.Roles)
                {
                    bFound = (role.Permissions.Where(p => p.PermissionDescription.ToLower() == requiredPermission.ToLower()).ToList().Count > 0);
                    if (bFound)
                        break;
                }
                return bFound;
            }
        }

    }
}
