using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoWithSQL.Controllers
{
    public class CSSController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CSS_Fixed()
        {
            return View();
        }

        public ActionResult CSS_HideALongText()
        {
            String str = "卡尔维诺生于古巴哈瓦那，随父母移居意大利。毕业于都灵大学文学系，第二次世界大战中积极参加反法西斯斗争。战后开始文学创作。1947年发表第一部长篇《通向蜘蛛巢的小路》。20世纪50年代起以幻想和离奇的手法写作小说，或反映现实中人的异化，或讽刺现实的种种荒谬滑稽。《两半的子爵》是他的代表作，这部小说同后来写的《树上的男爵》（1957）和《不存在的骑士》（1959）合辑为《我们的祖先》三部曲。20世纪六、七十年代卡尔维诺创作了《看不见的城市》（1972）和《宇宙喜剧》（1965）等。他还搜集整理了《意大利童话》。1985年9月，卡尔维诺突患脑溢血在意大利佩斯卡拉逝世，终年62岁，葬在地中海岸边的卡斯提格连小镇。";
            return View((object)str);
        }
    }
}