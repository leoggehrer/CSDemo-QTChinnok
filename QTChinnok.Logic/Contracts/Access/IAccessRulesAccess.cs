﻿//@CodeCopy
//MdStart
#if ACCOUNT_ON && ACCESSRULES_ON
namespace QTChinnok.Logic.Contracts.Access
{
    using CommonBase.Contracts;
    using TOutModel = Models.Access.AccessRule;

    public partial interface IAccessRulesAccess : IDataAccess<TOutModel>
    {
    }
}
#endif
//MdEnd
