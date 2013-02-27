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
        static ReferenceMap()
        {
            //Container = new UnityContainer();
            ////Model
            //Container.RegisterType<BaseEntity, Product>("Product");
            //Container.RegisterType<BaseEntity, Employee>("Employee");
            ////ViewModel
            ////Container.RegisterType<IListEntity, ListProductViewModel>("ListProduct");
            ////Container.RegisterType<IListEntity, ListWorkerViewModel>("ListWorker");
            ////Container.RegisterType<IAlterEntity, AlterEntityViewModel<Product>>("AlterProduct");
            ////Container.RegisterType<IAlterEntity, AlterEntityViewModel<Employee>>("AlterWorker");
            ////Container.RegisterType<AlterEntityViewModel<Product>, AlterProductViewModel>();
            ////Container.RegisterType<AlterEntityViewModel<Employee>, AlterWorkerViewModel>();
            //Container.RegisterInstance(typeof (AlterEntityViewModel<Product>), "RegisterProduct",
            //                           new AlterProductViewModel(ControlOperation.Register));
            //Container.RegisterInstance(typeof (AlterEntityViewModel<Product>), "UpdateProduct",
            //                           new AlterProductViewModel(ControlOperation.Update));
            //Container.RegisterInstance(typeof(AlterEntityViewModel<Employee>), "RegisterWorker",
            //                           new AlterWorkerViewModel(ControlOperation.Register));
            //Container.RegisterInstance(typeof(AlterEntityViewModel<Employee>), "UpdateWorker",
            //                           new AlterWorkerViewModel(ControlOperation.Update));

            //Container.RegisterType<ListEntityViewModel<Product>, ListProductViewModel>();
            //Container.RegisterType<ListEntityViewModel<Employee>, ListWorkerViewModel>();
            ////View
            //Container.RegisterType<UserControl,ListProductView>("ListProduct");
            //Container.RegisterType<UserControl, ListWorkerView>("ListWorker");
            ////Instances needed because of custom operations
            //Container.RegisterInstance(typeof (UserControl), "RegisterProduct",
            //                           new AlterProductView(new AlterProductViewModel(ControlOperation.Register)));
            //Container.RegisterInstance(typeof (UserControl), "RegisterWorker",
            //                           new AlterWorkerView(new AlterWorkerViewModel(ControlOperation.Register)));
            //Container.RegisterInstance(typeof (UserControl), "UpdateProduct",
            //                           new AlterProductView(new AlterProductViewModel(ControlOperation.Update)));
            //Container.RegisterInstance(typeof (UserControl), "UpdateWorker",
            //                           new AlterWorkerView(new AlterWorkerViewModel(ControlOperation.Update)));


            //Container.RegisterType<UserControl, AlterProductView>("AlterProduct");
            //Container.RegisterType<UserControl,AlterWorkerView>("AlterWorker");
            //Container.RegisterType<Window, FrameWindow>("Window");
            //Operations
        }

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