﻿//@CodeCopy
//MdStart
#if ACCOUNT_ON
namespace QTChinnok.Logic.Controllers.Account
{
    using TEntity = Entities.Account.Role;
    using TOutModel = Models.Account.Role;

    [Modules.Security.Authorize("SysAdmin", "AppAdmin")]
    internal sealed partial class RolesController : EntitiesController<TEntity, TOutModel>, Contracts.Account.IRolesAccess<TOutModel>
    {
        public RolesController()
        {
        }

        public RolesController(ControllerObject other) : base(other)
        {
        }

        protected override void BeforeActionExecute(ActionType actionType, TEntity entity)
        {
            if (actionType == ActionType.Insert)
            {
                entity.Guid = Guid.NewGuid();
            }
            else if (actionType == ActionType.Update)
            {
                using var ctrl = new RolesController();
                var dbEntity = ctrl.EntitySet.Find(entity.Id);

                if (dbEntity != null)
                {
                    entity.Guid = dbEntity.Guid;
                }
            }
            base.BeforeActionExecute(actionType, entity);
        }
    }
}
#endif
//MdEnd
