﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GeoQuiz.Database.DatabaseClasses
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="GeoDB")]
	public partial class GeoDBDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertCountry(Country instance);
    partial void UpdateCountry(Country instance);
    partial void DeleteCountry(Country instance);
    partial void InsertFlagNeighbour(FlagNeighbour instance);
    partial void UpdateFlagNeighbour(FlagNeighbour instance);
    partial void DeleteFlagNeighbour(FlagNeighbour instance);
    #endregion
		
		public GeoDBDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["GeoDBConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public GeoDBDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public GeoDBDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public GeoDBDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public GeoDBDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Country> Countries
		{
			get
			{
				return this.GetTable<Country>();
			}
		}
		
		public System.Data.Linq.Table<FlagNeighbour> FlagNeighbours
		{
			get
			{
				return this.GetTable<FlagNeighbour>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Countries")]
	public partial class Country : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _Name;
		
		private string _Capital;
		
		private string _Continent;
		
		private bool _IsSovereign;
		
		private EntitySet<FlagNeighbour> _FlagNeighbours;
		
		private EntitySet<FlagNeighbour> _FlagNeighbours1;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnCapitalChanging(string value);
    partial void OnCapitalChanged();
    partial void OnContinentChanging(string value);
    partial void OnContinentChanged();
    partial void OnIsSovereignChanging(bool value);
    partial void OnIsSovereignChanged();
    #endregion
		
		public Country()
		{
			this._FlagNeighbours = new EntitySet<FlagNeighbour>(new Action<FlagNeighbour>(this.attach_FlagNeighbours), new Action<FlagNeighbour>(this.detach_FlagNeighbours));
			this._FlagNeighbours1 = new EntitySet<FlagNeighbour>(new Action<FlagNeighbour>(this.attach_FlagNeighbours1), new Action<FlagNeighbour>(this.detach_FlagNeighbours1));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Capital", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Capital
		{
			get
			{
				return this._Capital;
			}
			set
			{
				if ((this._Capital != value))
				{
					this.OnCapitalChanging(value);
					this.SendPropertyChanging();
					this._Capital = value;
					this.SendPropertyChanged("Capital");
					this.OnCapitalChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Continent", DbType="NVarChar(10) NOT NULL", CanBeNull=false)]
		public string Continent
		{
			get
			{
				return this._Continent;
			}
			set
			{
				if ((this._Continent != value))
				{
					this.OnContinentChanging(value);
					this.SendPropertyChanging();
					this._Continent = value;
					this.SendPropertyChanged("Continent");
					this.OnContinentChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsSovereign", DbType="Bit NOT NULL")]
		public bool IsSovereign
		{
			get
			{
				return this._IsSovereign;
			}
			set
			{
				if ((this._IsSovereign != value))
				{
					this.OnIsSovereignChanging(value);
					this.SendPropertyChanging();
					this._IsSovereign = value;
					this.SendPropertyChanged("IsSovereign");
					this.OnIsSovereignChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Country_FlagNeighbour", Storage="_FlagNeighbours", ThisKey="Id", OtherKey="CountryId1")]
		public EntitySet<FlagNeighbour> FlagNeighbours
		{
			get
			{
				return this._FlagNeighbours;
			}
			set
			{
				this._FlagNeighbours.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Country_FlagNeighbour1", Storage="_FlagNeighbours1", ThisKey="Id", OtherKey="CountryId2")]
		public EntitySet<FlagNeighbour> FlagNeighbours1
		{
			get
			{
				return this._FlagNeighbours1;
			}
			set
			{
				this._FlagNeighbours1.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_FlagNeighbours(FlagNeighbour entity)
		{
			this.SendPropertyChanging();
			entity.Country = this;
		}
		
		private void detach_FlagNeighbours(FlagNeighbour entity)
		{
			this.SendPropertyChanging();
			entity.Country = null;
		}
		
		private void attach_FlagNeighbours1(FlagNeighbour entity)
		{
			this.SendPropertyChanging();
			entity.Country1 = this;
		}
		
		private void detach_FlagNeighbours1(FlagNeighbour entity)
		{
			this.SendPropertyChanging();
			entity.Country1 = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.FlagNeighbours")]
	public partial class FlagNeighbour : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private int _CountryId1;
		
		private int _CountryId2;
		
		private double _Distance;
		
		private EntityRef<Country> _Country;
		
		private EntityRef<Country> _Country1;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnCountryId1Changing(int value);
    partial void OnCountryId1Changed();
    partial void OnCountryId2Changing(int value);
    partial void OnCountryId2Changed();
    partial void OnDistanceChanging(double value);
    partial void OnDistanceChanged();
    #endregion
		
		public FlagNeighbour()
		{
			this._Country = default(EntityRef<Country>);
			this._Country1 = default(EntityRef<Country>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CountryId1", DbType="Int NOT NULL")]
		public int CountryId1
		{
			get
			{
				return this._CountryId1;
			}
			set
			{
				if ((this._CountryId1 != value))
				{
					if (this._Country.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCountryId1Changing(value);
					this.SendPropertyChanging();
					this._CountryId1 = value;
					this.SendPropertyChanged("CountryId1");
					this.OnCountryId1Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CountryId2", DbType="Int NOT NULL")]
		public int CountryId2
		{
			get
			{
				return this._CountryId2;
			}
			set
			{
				if ((this._CountryId2 != value))
				{
					if (this._Country1.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCountryId2Changing(value);
					this.SendPropertyChanging();
					this._CountryId2 = value;
					this.SendPropertyChanged("CountryId2");
					this.OnCountryId2Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Distance", DbType="Float NOT NULL")]
		public double Distance
		{
			get
			{
				return this._Distance;
			}
			set
			{
				if ((this._Distance != value))
				{
					this.OnDistanceChanging(value);
					this.SendPropertyChanging();
					this._Distance = value;
					this.SendPropertyChanged("Distance");
					this.OnDistanceChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Country_FlagNeighbour", Storage="_Country", ThisKey="CountryId1", OtherKey="Id", IsForeignKey=true)]
		public Country Country
		{
			get
			{
				return this._Country.Entity;
			}
			set
			{
				Country previousValue = this._Country.Entity;
				if (((previousValue != value) 
							|| (this._Country.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Country.Entity = null;
						previousValue.FlagNeighbours.Remove(this);
					}
					this._Country.Entity = value;
					if ((value != null))
					{
						value.FlagNeighbours.Add(this);
						this._CountryId1 = value.Id;
					}
					else
					{
						this._CountryId1 = default(int);
					}
					this.SendPropertyChanged("Country");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Country_FlagNeighbour1", Storage="_Country1", ThisKey="CountryId2", OtherKey="Id", IsForeignKey=true)]
		public Country Country1
		{
			get
			{
				return this._Country1.Entity;
			}
			set
			{
				Country previousValue = this._Country1.Entity;
				if (((previousValue != value) 
							|| (this._Country1.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Country1.Entity = null;
						previousValue.FlagNeighbours1.Remove(this);
					}
					this._Country1.Entity = value;
					if ((value != null))
					{
						value.FlagNeighbours1.Add(this);
						this._CountryId2 = value.Id;
					}
					else
					{
						this._CountryId2 = default(int);
					}
					this.SendPropertyChanged("Country1");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591