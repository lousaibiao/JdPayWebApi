using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JdPay.Data.CallbackModel;
using JdPay.Data.Request;
using JdPay.Data.Response;

namespace Site.Lib.IService
{
    public interface IJdService
    {
        Task<string> Test(string req);
        Task<DefrayPayRsp> DefrayPayAsync(DefrayPayReq req);
        Task<TradeQueryRsp> TradeQueryAsync(TradeQueryReq req);
        Task<ExpressContractedClientRsp> ExpressContractAsync(TypeVItem req);
        Task<FastPayRsp> FastPayAsync(TypeSItem req);
        Task<ExpressContractedClientRsp> ParseExpresssContractAsync(string content);
        Task<FastPayRsp> ParseFastPayAsync(string content);
        void VerifyDefrayPayCallback(DefrayPayCallbackQM qm);

    }
}
