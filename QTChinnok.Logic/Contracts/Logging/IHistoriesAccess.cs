//@CodeCopy
//MdStart
#if ACCOUNT_ON && LOGGING_ON
using CommonBase.Contracts;

namespace QTChinnok.Logic.Contracts.Logging
{
    public partial interface IActionLogsAccess<T> : IDataAccess<T>
    {
    }
}
#endif
//MdEnd
