using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NfcApi.Hubs;
using NfcApi.Models;
using System.Diagnostics;

namespace NfcApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NFCController : ControllerBase
    {
        private readonly IFreeSql freeSql;
        private readonly IHubContext<ChatHub> _hubContext;
        public NFCController(IFreeSql _freeSql, IHubContext<ChatHub> hubContext)
        {
            this.freeSql = _freeSql;
            this._hubContext = hubContext;
        }

        [HttpPost]
        public ResultModel<List<MusicMenu>> SelectMusicByAll() 
        {
            var music = freeSql.Select<MusicMenu>().ToList();
            if (music.Count > 0)
            {
                return new ResultModel<List<MusicMenu>>
                {
                    code = 200,
                    message = "获取成功",
                    data = music
                };
            }
            else 
            {
                return new ResultModel<List<MusicMenu>>
                {
                    code = 500,
                    message = "获取失败"
                };
            }
        }


        /// <summary>
        /// 根据指定ID获取音乐
        /// </summary>
        /// <param name="id">音乐编号</param>
        [HttpGet]
        public IActionResult SelectMusicByFirst(int id)
        {
            var music = freeSql.Select<MusicMenu>().Where(x => x.Id == id).First();


            if (music != null)
            {
                //向客户端发送消息
                _hubContext.Clients.All.SendAsync("Music", $"{music.Name}", $"{music.Url}");


                return Ok("send message success");
                //return new ResultModel<string>
                //{
                //    code = 200,
                //    message = "获取成功",
                //    data = music.Url
                //};
            }
            else
            {
                return Ok("send message error");
                //return new ResultModel<string>
                //{
                //    code = 500,
                //    message = "获取失败"
                //};
            }
        }

        [HttpPost]
        public ResultModel<List<MusicMenu>> SelectMusicByQuery(string name) 
        {
            var music = freeSql.Select<MusicMenu>()
                .Where(x => x.Name.Contains(name))
                .ToList();
            if (music.Count > 0)
            {
                return new ResultModel<List<MusicMenu>>
                {
                    code = 200,
                    message = "获取成功",
                    data = music
                };
            }
            else
            {
                return new ResultModel<List<MusicMenu>>
                {
                    code = 500,
                    message = "获取失败"
                };
            }
        }

        [HttpPost]
        public ResultModel<string> InsertMusic(MusicMenu model)
        {
            int count = freeSql.Insert<MusicMenu>().AppendData(model).ExecuteAffrows();
            if (count > 0)
            {
                return new ResultModel<string>
                {
                    code = 200,
                    message = "插入成功",
                    data = count.ToString()
                };
            }
            else
            {
                return new ResultModel<string>
                {
                    code = 500,
                    message = "插入失败",
                };
            }
        }


        [HttpPost]
        public ResultModel<string> UpdateMusic(int id,MusicMenu model)
        {
            int count = freeSql.Update<MusicMenu>()
                .Set(x => x.Id,model.Id)
                .Set(x => x.Url, model.Url)
                .Set(x => x.Url, model.Name)
                .Where(x => x.Id == id)
                .ExecuteAffrows();
            if (count > 0)
            {
                return new ResultModel<string>
                {
                    code = 200,
                    message = "更新成功",
                    data = count.ToString()
                };
            }
            else
            {
                return new ResultModel<string>
                {
                    code = 500,
                    message = "更新失败",
                };
            }
        }

        [HttpPost]
        public ResultModel<string> DeleteMusic(int id)
        {
            int count = freeSql.Delete<MusicMenu>()
                .Where(x => x.Id == id)
                .ExecuteAffrows();
            if (count > 0)
            {
                return new ResultModel<string>
                {
                    code = 200,
                    message = "删除成功",
                    data = count.ToString()
                };
            }
            else
            {
                return new ResultModel<string>
                {
                    code = 500,
                    message = "删除失败",
                };
            }
        }
    }
}
