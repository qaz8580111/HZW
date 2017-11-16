using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.DAL
{
    public class PerceptionDeviceDAL
    {
        public project01 GetMaxProject01()
        {
            using (hzwEntities db = new hzwEntities())
            {
                project01 model = db.project01.OrderByDescending(a => a.formattedCreated).FirstOrDefault();
                return model;
            }
        }

        public int AddProject01(List<project01> list)
        {
            using (hzwEntities db = new hzwEntities())
            {
                foreach (project01 item in list)
                {
                    project01 model = db.project01.Where(t => t.id == item.id).FirstOrDefault();
                    if (model == null)
                    {
                        db.project01.Add(item);
                        db.SaveChanges();
                    }
                }
                return 1;
            }
        }

        public project02 GetMaxProject02()
        {
            using (hzwEntities db = new hzwEntities())
            {
                project02 model = db.project02.OrderByDescending(a => a.formattedCreated).FirstOrDefault();
                return model;
            }
        }

        public int AddProject02(List<project02> list)
        {
            using (hzwEntities db = new hzwEntities())
            {
                foreach (project02 item in list)
                {
                    project02 model = db.project02.Where(t => t.id == item.id).FirstOrDefault();
                    if (model == null)
                    {
                        if (item.msgId == "50331652")
                        {
                            item.x = 326694.541278314;
                            item.y = 3356777.96124326;
                        }
                        else if (item.msgId == "50331653")
                        {
                            item.x = 326718.573544576;
                            item.y = 3356685.51122654;
                        }
                        else if (item.msgId == "50331654")
                        {
                            item.x = 326679.354316454;
                            item.y = 3356882.02901353;
                        }
                        else if (item.msgId == "50331655")
                        {
                            item.x = 326713.419856078;
                            item.y = 3356712.4370975;
                        }
                        else if (item.msgId == "50331656")
                        {
                            item.x = 326686.657715592;
                            item.y = 3356905.65116186;
                        }
                        else if (item.msgId == "50331657")
                        {
                            item.x = 326664.528011292;
                            item.y = 3357033.01276364;
                        }
                        else if (item.msgId == "50331658")
                        {
                            item.x = 326666.707024088;
                            item.y = 3357061.15333142;
                        }
                        else if (item.msgId == "50331659")
                        {
                            item.x = 326645.891779465;
                            item.y = 3357113.73007947;
                        }
                        else if (item.msgId == "50331660")
                        {
                            item.x = 326627.212915618;
                            item.y = 3357246.25055097;
                        }
                        else if (item.msgId == "50331661")
                        {
                            item.x = 326695.839395112;
                            item.y = 3356829.41021954;
                        }
                        db.project02.Add(item);
                        db.SaveChanges();
                    }
                }
                return 1;
            }
        }

        public project03 GetMaxProject03()
        {
            using (hzwEntities db = new hzwEntities())
            {
                project03 model = db.project03.OrderByDescending(a => a.formattedCreated).FirstOrDefault();
                return model;
            }
        }

        public int AddProject03(List<project03> list)
        {
            using (hzwEntities db = new hzwEntities())
            {
                foreach (project03 item in list)
                {
                    project03 model = db.project03.Where(t => t.id == item.id).FirstOrDefault();
                    if (model == null)
                    {
                        if (item.msgId == "67108866")
                        {
                            item.x = 329313.16;
                            item.y = 3357611.62;
                            item.address = "海川大道与滨海二路";
                        }
                        else if (item.msgId == "67108865")
                        {
                            item.x = 328667.27;
                            item.y = 3359017.01;
                            item.address = "芦汀路滨海五路";
                        }

                        db.project03.Add(item);
                        db.SaveChanges();
                    }
                }
                return 1;
            }
        }
    }
}
