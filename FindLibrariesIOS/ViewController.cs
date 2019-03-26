using System;
using System.Collections.Generic;
using UIKit;
using Accelerate;
using Accounts;
using AddressBook;
using AddressBookUI;
using AdSupport;
using AssetsLibrary;
using AudioToolbox;
using AudioUnit;
using AVFoundation;
using AVKit;
using CloudKit;
using Contacts;
using ContactsUI;
using CoreAnimation;
using CoreAudioKit;
using CoreBluetooth;
using CoreData;
using CoreFoundation;
using CoreGraphics;
using CoreImage;
using CoreLocation;
using CoreMedia;
using CoreMidi;
using CoreMotion;
using CoreServices;
using CoreSpotlight;
using CoreTelephony;
using CoreText;
using CoreVideo;
using EventKit;
using EventKitUI;
using ExternalAccessory;
using FindLibrariesIOS;
using Foundation;
using GLKit;
using GameKit;
using GameplayKit;
using GameController;
using HomeKit;
using HealthKit;
using HealthKitUI;
using iAd;
using ImageIO;
using JavaScriptCore;
using LocalAuthentication;
using MapKit;
using MediaAccessibility;
using MediaPlayer;
using MediaToolbox;
using MessageUI;
using Metal;
using MetalKit;
using MetalPerformanceShaders;
using MobileCoreServices;
using ModelIO;
using MultipeerConnectivity;
using NetworkExtension;
using NewsstandKit;
using NotificationCenter;
using OpenTK;
using OpenGLES;
using ObjCRuntime;
using PassKit;
using Photos;
using PhotosUI;
using PushKit;
using QuickLook;
using ReplayKit;
using SafariServices;
using SceneKit;
using Security;
using Social;
using SpriteKit;
using StoreKit;
using SystemConfiguration;
using System.Reflection;

namespace FindLibrariesIOS
{
	public partial class ViewController : UIViewController
	{
		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			//Foundation.NSKeyedUnarchiver+_NSKeyedUnarchiverDelegate  
			// Perform any additional setup after loading the view, typically from a nib.
			foreach (var type in getCustomTypes())
			{
				//Console.WriteLine(""+s);
				var attributes = type.GetCustomAttributes(true);
				foreach (var attr in attributes)
				{
					
					var isRegisterAttribute = attr.GetType().Equals(typeof(RegisterAttribute));
					//Console.WriteLine("" + );
					if ( isRegisterAttribute )
					{
						Console.WriteLine("type:" + type.ToString());
						Console.WriteLine("Attribute:" + attr.ToString());
						var registerAttr = (RegisterAttribute)attr;
						Console.WriteLine("Name:" + registerAttr.Name);
						Console.WriteLine("IsWrapper:" + registerAttr.IsWrapper);
						Console.WriteLine("SkipRegistration:" + registerAttr.SkipRegistration);
						Console.WriteLine("TypeId:" + registerAttr.TypeId);
						//Console.WriteLine("Name" + registerAttr.);
					}
				}
				MethodInfo [] methods =type.GetMethods(System.Reflection.BindingFlags.Default);

				//methodAttributes.
				foreach (var method in methods)
				{
					MethodAttributes methodAttributes = method.Attributes;
					Console.WriteLine("Attributes:" + methodAttributes.ToString());
				}

				ConstructorInfo[] Constructors = type.GetConstructors(System.Reflection.BindingFlags.Default);
				foreach (var constructor in Constructors)
				{
					MethodAttributes constructorsAttributes = constructor.Attributes;
					Console.WriteLine("Attributes:" + constructorsAttributes.ToString());
				}
				PropertyInfo[] Propertys = type.GetProperties(System.Reflection.BindingFlags.Default);
				foreach (var property in Propertys)
				{
					PropertyAttributes propertyAttributes = property.Attributes;
					Console.WriteLine("Attributes:" + propertyAttributes.ToString());
				}
			}
		}

		public static List<Type> getCustomTypes()
		{
			var assemblies = AppDomain.CurrentDomain.GetAssemblies();
			////HACK
			//var typesString = DotNetRuntimeTypesString + "\n" + DotNetTypesString;
			//string[] DotNetTypesNames = typesString.Split('\n');
			//string[] DotNetTypesNames = DotNetTypesString.Split('\n');

			//for (int i = 0; i < DotNetTypesNames.Length; i++)
			//{
			//	DotNetTypesNames[i] = DotNetTypesNames[i].Trim();
			//}

			//foreach (var s in DotNetTypesNames)
			//{
			//	Console.WriteLine("" + s);
			//}

			//var DotNetTypesNames = getAllTypesFullName();

			List<Type> typesList = new List<Type>();
			foreach (var assembly in assemblies)
			{
				var types = assembly.GetTypes();
				foreach (var type in types)
				{
					var name = type.FullName;
					if (
						//Hack
						!name.StartsWith("System.", StringComparison.CurrentCulture)
						&&
						!name.StartsWith("Microsoft.", StringComparison.CurrentCulture)
						&&
						!name.StartsWith("Mono.", StringComparison.CurrentCulture)
						&&
						!name.Equals("AssemblyRef")
						&&
						!name.Equals("SR")
						&&
						!name.Equals("Consts")
						&&
						!name.Equals("Locale")
						&&
						!name.StartsWith("<PrivateImplementationDetails>", StringComparison.CurrentCulture)
					//&&
					//   ! DotNetTypesNames.Contains(type.FullName)
					)
					{
						//Console.WriteLine("" + type.FullName);
						typesList.Add(type);
					}
				}
			}
			return typesList;
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

