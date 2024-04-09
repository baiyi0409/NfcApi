using FreeSql.DataAnnotations;

namespace NfcApi.Models
{
    /// <summary>
    /// 音乐菜单
    /// </summary>
    public class MusicMenu
    {

        /// <summary>
        /// 序号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 歌曲名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 访问地址
        /// </summary>
        public string Url { get; set; }
    }
}
