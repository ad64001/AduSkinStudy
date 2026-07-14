using AduSkin.Demo.Data.Enum;
using AduSkin.Demo.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AduSkin.Demo.ViewModel
{
   public partial class TestASViewModel : ObservableObject
   {
      public TestASViewModel() {
         AllSupports.Add(new TestA("不愿意透露姓名的网友", "tencent://message/?uin=870856195&Site=&Menu=yes", "http://q.qlogo.cn/headimg_dl?bs=qq&dst_uin=870856195&spec=100", "", new string[] { SupportType.Money.ToString() + "：100元" }));
         AllSupports.Add(new TestA("沙漠尽头的狼 dotnet9.com", "tencent://message/?uin=632871194&Site=&Menu=yes", "http://q.qlogo.cn/headimg_dl?bs=qq&dst_uin=632871194&spec=100", "", new string[] { SupportType.Skill.ToString(), SupportType.Extension.ToString() }));
         AllSupports.Add(new TestA("关关", "tencent://message/?uin=2453966523&Site=&Menu=yes", "http://q.qlogo.cn/headimg_dl?bs=qq&dst_uin=2453966523&spec=100", "", new string[] { SupportType.Skill.ToString(), SupportType.Extension.ToString() }));
         AllSupports.Add(new TestA("Tom", "tencent://message/?uin=17379620&Site=&Menu=yes", "http://q.qlogo.cn/headimg_dl?bs=qq&dst_uin=17379620&spec=100", "", new string[] { SupportType.Skill.ToString(), SupportType.Extension.ToString(), SupportType.Money.ToString() + "：350元" }));
         AllSupports.Add(new TestA("KING", "tencent://message/?uin=1061973727&Site=&Menu=yes", "http://q.qlogo.cn/headimg_dl?bs=qq&dst_uin=1061973727&spec=100", "", new string[] { SupportType.Skill.ToString() }));
         AllSupports.Add(new TestA("CJ", "tencent://message/?uin=836904362&Site=&Menu=yes", "http://q.qlogo.cn/headimg_dl?bs=qq&dst_uin=836904362&spec=100", "", new string[] { SupportType.Skill.ToString() }));
         AllSupports.Add(new TestA("FOX-Yu", "", "http://q.qlogo.cn/headimg_dl?bs=qq&dst_uin=435892115&spec=100", "", new string[] { SupportType.Money.ToString() + "：88元" }));
         AllSupports.Add(new TestA("不愿意透露姓名的网友", "", "http://q.qlogo.cn/headimg_dl?bs=qq&dst_uin=2603473237&spec=100", "", new string[] { SupportType.Money.ToString() + "：300元" }));
         AllSupports.Add(new TestA("那年", "", "http://q.qlogo.cn/headimg_dl?bs=qq&dst_uin=407564418&spec=100", "", new string[] { SupportType.Money.ToString() + "：100元" }));
         AllSupports.Add(new TestA("不染", "", "http://q.qlogo.cn/headimg_dl?bs=qq&dst_uin=906247584&spec=100", "", new string[] { SupportType.Money.ToString() + "：50元" }));
         AllSupports.Add(new TestA("MiFaFa", "", "http://q.qlogo.cn/headimg_dl?bs=qq&dst_uin=2447786794&spec=100", "", new string[] { SupportType.Money.ToString() + "：66元" }));
         AllSupports.Add(new TestA("✘小浪", "", "http://q.qlogo.cn/headimg_dl?bs=qq&dst_uin=2398387230&spec=100", "", new string[] { SupportType.Money.ToString() + "：100元" }));
         AllSupports.Add(new TestA("懒猫", "", "http://q.qlogo.cn/headimg_dl?bs=qq&dst_uin=8080697&spec=100", "", new string[] { SupportType.Money.ToString() + "：100元" }));

      }

      /// <summary>
      /// 赞助人
      /// </summary>
      [ObservableProperty]
      private ObservableCollection<TestA> _allSupports = new ObservableCollection<TestA>();

      /// <summary>
      /// 命令Command
      /// </summary>
      [RelayCommand]
      public void Open(string e)
      {
         switch (e)
         {
            case "Reward":
               IsOpenReward = true;
               return;
         }
      }
      [ObservableProperty]
      private bool _isOpenReward;
   }
}
