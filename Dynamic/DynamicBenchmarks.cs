using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;

namespace Benchmarks
{
    [MemoryDiagnoser(false)]
    public class DynamicBenchmarks
    {
        ReservationDefault reservationDefault;
        dynamic dynamicReservation; 

        public DynamicBenchmarks()
        {
            reservationDefault = new ReservationDefault()
            {
                ProfileInfos = new List<ReservationDefault.ReservationProfileInfos>() { new ReservationDefault.ReservationProfileInfos()
                {
                    Personal = new ReservationDefault.ReservationProfileInfos.ReservationProfileInfosPersonal()
                    {
                        Givenname = "Vorname",
                        Lastname = "Nachname"
                    }
                }
                }
            };

            dynamicReservation = (dynamic)reservationDefault; 
        }

        [Benchmark]
        public string Typed()
        {
            return GetLabelBig(reservationDefault); 
        }

        [Benchmark]
        public string UnTyped()
        {
            return GetLabelBig("Entity.Reservation", dynamicReservation); 
        }

        public string GetLabelBig(IBaseObject entity)
        {
            return entity switch
            {
                ReservationDefault r => r.ProfileInfos[0]?.Personal?.Givenname ?? "" + r.ProfileInfos[0].Personal.Lastname,
                ReservationTicketing r => r.ProfileInfos.Shipping.Givenname + r.ProfileInfos.Shipping.Lastname,
                _ => throw new Exception("Repository Translation not added yet."),
            };
        }

        public string GetLabelBig(string repositoryName, dynamic entity)
        {
            switch (repositoryName)
            {
                case "Entity.Reservation":
                    var personal = entity.ProfileInfos[0].Personal;
                    return $"{personal.Givenname} {personal.Lastname}";
                case "Entity.Ticketing.Reservation":
                    var shipping = entity.ProfileInfos.Shipping;
                    return $"{shipping.Givenname} {shipping.Lastname}";
                default:
                    throw new Exception("Repository Translation not added yet.");
            }
        }
    }

    public class ReservationDefault : IBaseObject
    {
        public virtual IList<ReservationProfileInfos> ProfileInfos { set; get; }
        public partial class ReservationProfileInfos
        {
            public virtual ReservationProfileInfosPersonal Personal { set; get; }

            public partial class ReservationProfileInfosPersonal
            {
                public virtual System.Int32? Age { set; get; }
                public virtual System.DateTime? DateOfBirth { set; get; }
                public virtual System.String Gender { set; get; }
                public virtual System.String Givenname { set; get; }
                public virtual System.String Language { set; get; }
                public virtual System.String Lastname { set; get; }
                public virtual System.String Middlename { set; get; }
                public virtual System.String Salutation { set; get; }
                public virtual System.String Title { set; get; }
            }

        }
    }

    public interface IBaseObject
    {
    }

    public class ReservationTicketing : IBaseObject
    {
        public virtual ReservationProfileInfos ProfileInfos { set; get; }

        public partial class ReservationProfileInfos
        {
            #region Properties

            public virtual ReservationProfileInfosShipping Shipping { set; get; }

            #endregion Properties

            public partial class ReservationProfileInfosShipping
            {
                public virtual System.String Givenname { set; get; }
                public virtual System.String Lastname { set; get; }
            }
        }
    }
}
