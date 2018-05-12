using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Essensoft.AspNetCore.Payment.Alipay;
using Essensoft.AspNetCore.Payment.Alipay.Domain;
using Essensoft.AspNetCore.Payment.Alipay.Notify;
using Essensoft.AspNetCore.Payment.Alipay.Request;
using Microsoft.AspNetCore.Mvc;

namespace Dnc.MvcApp.Controllers
{


    public class AlipayController : Controller
    {
        // 支付宝请求客户端(用于处理请求与其响应)
        private readonly AlipayClient _client = null;

        // 支付宝通知客户端(用于解析异步通知或同步跳转)
        private readonly AlipayNotifyClient _notifyClient = null;

        // 赋值依赖注入对象
        public AlipayController(AlipayClient client, AlipayNotifyClient notifyClient)
        {
            _client = client;
            _notifyClient = notifyClient;
        }

        [HttpPost]
        public async Task<IActionResult> PagePay(string out_trade_no, string subject, string total_amount, string body, string product_code, string notify_url, string return_url)
        {
            // 组装模型
            var model = new AlipayTradePagePayModel()
            {
                Body = body,
                Subject = subject,
                TotalAmount = total_amount,
                OutTradeNo = out_trade_no,
                ProductCode = product_code,
            };

            var req = new AlipayTradePagePayRequest();

            // 设置请求参数
            req.SetBizModel(model);

            // 设置异步通知URL
            req.SetNotifyUrl(notify_url);

            // 设置同步跳转URL
            req.SetReturnUrl(return_url);

            // 页面请求处理 传入 'GET' 返回的 response.Body 为 URL, 'POST' 返回的 response.Body 为 HTML.
            var response = await _client.PageExecuteAsync(req, null, "GET");

            // 重定向到支付宝电脑网页支付页面.
            return Redirect(response.Body);
        }
        /// <summary>
        /// 电脑网页支付-同步跳转
        /// 常用于展示订单支付状态页，建议在异步通知统一做业务处理，而不是在此处.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> PagePayReturn()
        {
            try
            {
                // 以 AlipayTradePagePayReturnResponse 类型 解析
                var notify = await _notifyClient.ExecuteAsync<AlipayTradePagePayReturnResponse>(Request);
                return Content("成功:" + notify.OutTradeNo);
            }
            catch
            {
                return Content("参数异常/验签失败");
            }
        }

        /// <summary>
        /// 电脑网页支付-异步通知
        /// 常用于订单业务处理
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PagePayNotify()
        {
            try
            {
                // 以 AlipayTradePagePayNotifyResponse 类型 解析
                var notify = await _notifyClient.ExecuteAsync<AlipayTradePagePayNotifyResponse>(Request);
                if ("TRADE_SUCCESS" == notify.TradeStatus) // 订单是否交易完成
                {
                    // 业务代码
                    // ...
                    // ...

                    //返回给支付宝成功内容，停止继续通知
                    return Content("success", "text/plain");
                }
                // 订单其他状态均返回给支付宝空内容.
                return NoContent();
            }
            catch
            {
                // 参数异常/验签失败均返回给支付宝空内容.
                return NoContent();
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}