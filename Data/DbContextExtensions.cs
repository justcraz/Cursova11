using Microsoft.CodeAnalysis.CSharp.Syntax;
using PharmaKursWork.Models;

namespace PharmaKursWork.Data
{
    public static class DbContextExtensions
    {
        public static void EnsureDatabaseSeeded(this DataContext dataContext)
        {
            // додавання одиниць вимірів
            if (!dataContext.UnitMeasures.Any())
            {
                dataContext.UnitMeasures.AddRange(new UnitMeasure[] {
                    new UnitMeasure()
                    {
                        Name= "КГ",
                        Desc= "Кілограми",
                    },
                    new UnitMeasure()
                    {
                        Name= "ГМ",
                        Desc= "грами",
                    },
                    new UnitMeasure()
                    {
                        Name= "Л",
                        Desc= "Літри",
                    },
                });
                dataContext.SaveChanges();
            }

            // додавання товарних груп
            if (!dataContext.CommodityGroups.Any())
            {
                dataContext.CommodityGroups.AddRange(new CommodityGroup[] {
                    new CommodityGroup()
                    {
                        Name = "Таблетка",
                        Desc = "тверде",
                    },
                    new CommodityGroup()
                    {
                        Name = "Сироп",
                        Desc = "рідке",
                    },
                    new CommodityGroup()
                    {
                        Name = "Гормоны",
                        Desc = "????",
                    },
                });
                dataContext.SaveChanges();
            }

            // додавання лабораторій
            if (!dataContext.Laboratories.Any())
            {
                dataContext.Laboratories.AddRange(new Laboratory[] {
                    new Laboratory()
                    {
                        Name = "Зі",
                        Adress = "чернівці, кобилянська 12",
                    },
                    new Laboratory()
                    {
                        Name = "Форта",
                        Adress = "соборна 3",
                    },
                });
                dataContext.SaveChanges();
            }

            // Додавання ліків
            if (!dataContext.Meds.Any())
            {
                var lab = dataContext.Laboratories.FirstOrDefault();
                var commodityGroup = dataContext.CommodityGroups.FirstOrDefault();
                var unit = dataContext.UnitMeasures.FirstOrDefault();

                dataContext.Meds.AddRange(new Med[] {
                    new Med()
                    {
                        Name = "Еспумизан",
                        StartExploring = DateTime.Now,
                        Expenses = 100,
                        UnitMeasureId = unit.Id,
                        CommodityGroupId = commodityGroup.Id,
                        LabratoryId = lab.Id,
                    },
                    new Med()
                    {
                        Name = "Валер'яна",
                        StartExploring = DateTime.Now,
                        Expenses = 1022222220,
                        UnitMeasureId = unit.Id,
                        CommodityGroupId = commodityGroup.Id,
                        LabratoryId = lab.Id,
                    },
                    new Med()
                    {
                        Name = "Доктор Мом",
                        StartExploring = DateTime.Now,
                        Expenses = 102220,
                        UnitMeasureId = unit.Id,
                        CommodityGroupId = commodityGroup.Id,
                        LabratoryId = lab.Id,
                    },
                });
                dataContext.SaveChanges();
            }

            // Додавання працівників лабораторії
            if (!dataContext.LaboratoryEmployees.Any())
            {
                var lab = dataContext.Laboratories.FirstOrDefault();

                dataContext.LaboratoryEmployees.AddRange(new LaboratoryEmployee[] {
                    new LaboratoryEmployee()
                    {
                        FirstName = "Іван",
                        LastName = "Бурий",
                        PhoneNumber = "+380994728614",
                        Email = "ivan@gmail.com",
                        Adress = "Чернівці",
                        LabratoryId = lab.Id,
                    },
                    new LaboratoryEmployee()
                    {
                        FirstName = "Джеймс",
                        LastName = "Бонд",
                        PhoneNumber = "+380992569475",
                        Email = "jaja@gmail.com",
                        Adress = "Чернівці",
                        LabratoryId = lab.Id,
                    },
                    new LaboratoryEmployee()
                    {
                        FirstName = "Юрій",
                        LastName = "Багрій",
                        PhoneNumber = "+380504798215",
                        Email = "yur@gmail.com",
                        Adress = "Хотин",
                        LabratoryId = lab.Id,
                    },
                    new LaboratoryEmployee()
                    {
                        FirstName = "Карп",
                        LastName = "Марковський",
                        PhoneNumber = "+380502147965",
                        Email = "karp@gmail.com",
                        Adress = "Магала",
                        LabratoryId = lab.Id,
                    },
                });
                dataContext.SaveChanges();

                var labEmployeers = (from l in dataContext.LaboratoryEmployees select l).ToList();

                dataContext.Scientists.AddRange(new Scientist[]
                {
                    new Scientist()
                    {
                        LaboratoryEmployeeId = labEmployeers[0].Id,
                        ResponsibleForDevice = "Прилад1",
                        DirectionDevelopment = "Таблетки",
                    },
                    new Scientist()
                    {
                        LaboratoryEmployeeId = labEmployeers[1].Id,
                        ResponsibleForDevice = "Прилад2",
                        DirectionDevelopment = "Рідини",
                    }
                });

                dataContext.TechStaffs.AddRange(new TechStaff[]
                {
                    new TechStaff()
                    {
                        LaboratoryEmployeeId = labEmployeers[2].Id,
                        MaintainsDevice = "Прилад1",
                        HasYourInstruments = true,
                    },
                    new TechStaff()
                    {
                        LaboratoryEmployeeId = labEmployeers[3].Id,
                        MaintainsDevice = "Прилад2",
                        HasYourInstruments = false,
                    },
                });
                dataContext.SaveChanges();
            }


            if (!dataContext.Users.Any())
            {
                dataContext.Users.AddRange(new User[]
                {
                    new User()
                    {
                        Username = "admin",
                        Password = "123456",
                        authenticationKey = new Guid(),
                        isAdmin= true,
                    },
                    new User()
                    {
                        Username = "UserUser",
                        Password = "123456",
                        authenticationKey = new Guid(),
                        isAdmin= false,
                    }
                });

                dataContext.SaveChanges();
            }

            if (!dataContext.Challenges.Any())
            {
                var med = dataContext.Meds.FirstOrDefault();
                var scientist = dataContext.Scientists.FirstOrDefault();
                var techStaff = dataContext.TechStaffs.FirstOrDefault();


                dataContext.Challenges.AddRange(new Challenge[]
                {
                    new Challenge()
                    {
                        Name = "Випробування",
                        ChallegesStart = DateTime.Now,
                        MedsId = med.Id,
                        ScientistId = scientist.LaboratoryEmployeeId,
                        TechStaffId = techStaff.LaboratoryEmployeeId,
                    },
                    new Challenge()
                    {
                        Name = "Випробування2",
                        ChallegesStart = DateTime.Now,
                        MedsId = med.Id,
                        ScientistId = scientist.LaboratoryEmployeeId,
                        TechStaffId = techStaff.LaboratoryEmployeeId,
                    }
                });

                dataContext.SaveChanges();
            }

        }
    }
}
