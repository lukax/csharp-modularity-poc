#region Usings

using System;
using System.Windows.Controls;
using LOB.Domain.Base;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.Business.Framework
{
    public enum ReferenceMode
    {
        Alter,
        List
    }

    public static class ReferenceMap
    {
        public static UnityContainer Container { get; private set; }

        public static Object Resolve<T>(String parameter)
        {
            return Container.Resolve(typeof (T), parameter);
        }

        public static Object ResolveView(String indentifier)
        {
            return Container.Resolve<UserControl>(indentifier);
        }

        public static Object ResolveView<T>(T entity) where T : BaseEntity
        {
            UserControl control = null;
            //if (typeof(T) == typeof(Product))
            //    control = Container.Resolve<UserControl>("UpdateProduct");
            //else if (typeof(T) == typeof(Employee))
            //    control = Container.Resolve<UserControl>("UpdateWorker");
            //else
            //    throw new ArgumentException("Resolve does not support this type of Entity");

            //var data = ((AlterEntityViewModel<T>)control.DataContext);
            //data.Entity = entity;
            return control;
        }
    }
}