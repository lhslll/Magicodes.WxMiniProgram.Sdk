﻿// ======================================================================
// 
//           Copyright (C) 2019-2030 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : TestBase.cs
//           description :
// 
//           created by 雪雁 at  2019-11-06 11:38
//           文档官网：https://docs.xin-lai.com
//           公众号教程：麦扣聊技术
//           QQ群：85318032（编程交流）
//           Blog：http://www.cnblogs.com/codelove/
// 
// ======================================================================

using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.MsDependencyInjection;
using Magicodes.WxMiniProgram.Sdk.Configs;
using Magicodes.WxMiniProgram.Sdk.Installers;
using Microsoft.Extensions.DependencyInjection;

namespace Test
{
    public class TestBase
    {
        public TestBase()
        {
            Container = new WindsorContainer();
            Container.Install(new WxMiniProgramSdkInstaller());

            //设置配置
            Container.Register(
                Component.For<IMiniProgramConfig>()
                    .Instance(new DefaultMiniProgramConfig
                    {
                        MiniProgramAppId = "wxea60106d88226354",
                        MiniProgramAppSecret = "63b9f4d8d7a4e88a0c42955f1a9296ca"
                    })
                    .LifeStyle.Singleton
            );

            var services = new ServiceCollection();
            services.AddDistributedMemoryCache();
            WindsorRegistrationHelper.CreateServiceProvider(Container, services);
        }

        protected IWindsorContainer Container { get; set; }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
    }
}