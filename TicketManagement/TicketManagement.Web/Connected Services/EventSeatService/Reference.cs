﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TicketManagement.Web.EventSeatService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="EventSeat", Namespace="http://schemas.datacontract.org/2004/07/TicketManagement.WcfService.Contracts")]
    [System.SerializableAttribute()]
    public partial class EventSeat : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int EventAreaIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int NumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int RowField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private TicketManagement.Web.EventSeatService.EventSeatState StateField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int EventAreaId {
            get {
                return this.EventAreaIdField;
            }
            set {
                if ((this.EventAreaIdField.Equals(value) != true)) {
                    this.EventAreaIdField = value;
                    this.RaisePropertyChanged("EventAreaId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Number {
            get {
                return this.NumberField;
            }
            set {
                if ((this.NumberField.Equals(value) != true)) {
                    this.NumberField = value;
                    this.RaisePropertyChanged("Number");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Row {
            get {
                return this.RowField;
            }
            set {
                if ((this.RowField.Equals(value) != true)) {
                    this.RowField = value;
                    this.RaisePropertyChanged("Row");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public TicketManagement.Web.EventSeatService.EventSeatState State {
            get {
                return this.StateField;
            }
            set {
                if ((this.StateField.Equals(value) != true)) {
                    this.StateField = value;
                    this.RaisePropertyChanged("State");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="EventSeatState", Namespace="http://schemas.datacontract.org/2004/07/TicketManagement.DAL.Constants")]
    public enum EventSeatState : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Free = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        InBasket = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Sold = 2,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="EventSeatService.IEventSeatContract")]
    public interface IEventSeatContract {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEventSeatContract/GetEventSeats", ReplyAction="http://tempuri.org/IEventSeatContract/GetEventSeatsResponse")]
        TicketManagement.Web.EventSeatService.EventSeat[] GetEventSeats();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEventSeatContract/GetEventSeats", ReplyAction="http://tempuri.org/IEventSeatContract/GetEventSeatsResponse")]
        System.Threading.Tasks.Task<TicketManagement.Web.EventSeatService.EventSeat[]> GetEventSeatsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEventSeatContract/GetEventSeat", ReplyAction="http://tempuri.org/IEventSeatContract/GetEventSeatResponse")]
        TicketManagement.Web.EventSeatService.EventSeat GetEventSeat(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEventSeatContract/GetEventSeat", ReplyAction="http://tempuri.org/IEventSeatContract/GetEventSeatResponse")]
        System.Threading.Tasks.Task<TicketManagement.Web.EventSeatService.EventSeat> GetEventSeatAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEventSeatContract/UpdateEventSeat", ReplyAction="http://tempuri.org/IEventSeatContract/UpdateEventSeatResponse")]
        void UpdateEventSeat(TicketManagement.Web.EventSeatService.EventSeat entity);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEventSeatContract/UpdateEventSeat", ReplyAction="http://tempuri.org/IEventSeatContract/UpdateEventSeatResponse")]
        System.Threading.Tasks.Task UpdateEventSeatAsync(TicketManagement.Web.EventSeatService.EventSeat entity);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEventSeatContract/GetEventSeatsByEventSeatIds", ReplyAction="http://tempuri.org/IEventSeatContract/GetEventSeatsByEventSeatIdsResponse")]
        TicketManagement.Web.EventSeatService.EventSeat[] GetEventSeatsByEventSeatIds(int[] idArray);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEventSeatContract/GetEventSeatsByEventSeatIds", ReplyAction="http://tempuri.org/IEventSeatContract/GetEventSeatsByEventSeatIdsResponse")]
        System.Threading.Tasks.Task<TicketManagement.Web.EventSeatService.EventSeat[]> GetEventSeatsByEventSeatIdsAsync(int[] idArray);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEventSeatContract/GetEventSeatsByEventAreaId", ReplyAction="http://tempuri.org/IEventSeatContract/GetEventSeatsByEventAreaIdResponse")]
        TicketManagement.Web.EventSeatService.EventSeat[] GetEventSeatsByEventAreaId(int eventAreaId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEventSeatContract/GetEventSeatsByEventAreaId", ReplyAction="http://tempuri.org/IEventSeatContract/GetEventSeatsByEventAreaIdResponse")]
        System.Threading.Tasks.Task<TicketManagement.Web.EventSeatService.EventSeat[]> GetEventSeatsByEventAreaIdAsync(int eventAreaId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IEventSeatContractChannel : TicketManagement.Web.EventSeatService.IEventSeatContract, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class EventSeatContractClient : System.ServiceModel.ClientBase<TicketManagement.Web.EventSeatService.IEventSeatContract>, TicketManagement.Web.EventSeatService.IEventSeatContract {
        
        public EventSeatContractClient() {
        }
        
        public EventSeatContractClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public EventSeatContractClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public EventSeatContractClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public EventSeatContractClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public TicketManagement.Web.EventSeatService.EventSeat[] GetEventSeats() {
            return base.Channel.GetEventSeats();
        }
        
        public System.Threading.Tasks.Task<TicketManagement.Web.EventSeatService.EventSeat[]> GetEventSeatsAsync() {
            return base.Channel.GetEventSeatsAsync();
        }
        
        public TicketManagement.Web.EventSeatService.EventSeat GetEventSeat(int id) {
            return base.Channel.GetEventSeat(id);
        }
        
        public System.Threading.Tasks.Task<TicketManagement.Web.EventSeatService.EventSeat> GetEventSeatAsync(int id) {
            return base.Channel.GetEventSeatAsync(id);
        }
        
        public void UpdateEventSeat(TicketManagement.Web.EventSeatService.EventSeat entity) {
            base.Channel.UpdateEventSeat(entity);
        }
        
        public System.Threading.Tasks.Task UpdateEventSeatAsync(TicketManagement.Web.EventSeatService.EventSeat entity) {
            return base.Channel.UpdateEventSeatAsync(entity);
        }
        
        public TicketManagement.Web.EventSeatService.EventSeat[] GetEventSeatsByEventSeatIds(int[] idArray) {
            return base.Channel.GetEventSeatsByEventSeatIds(idArray);
        }
        
        public System.Threading.Tasks.Task<TicketManagement.Web.EventSeatService.EventSeat[]> GetEventSeatsByEventSeatIdsAsync(int[] idArray) {
            return base.Channel.GetEventSeatsByEventSeatIdsAsync(idArray);
        }
        
        public TicketManagement.Web.EventSeatService.EventSeat[] GetEventSeatsByEventAreaId(int eventAreaId) {
            return base.Channel.GetEventSeatsByEventAreaId(eventAreaId);
        }
        
        public System.Threading.Tasks.Task<TicketManagement.Web.EventSeatService.EventSeat[]> GetEventSeatsByEventAreaIdAsync(int eventAreaId) {
            return base.Channel.GetEventSeatsByEventAreaIdAsync(eventAreaId);
        }
    }
}
