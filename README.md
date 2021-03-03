# Abyssal Common Libraries (ACL)
A set of common classes and utilities used across my projects.  
  
  
### 🛠 Available APIs
**Extensions**
- [`EmptyDisposable`](https://github.com/abyssal/common/blob/master/Abyssal.Common/EmptyDisposable.cs): An implementation of `System.IDisposable` that does nothing.
- [`HiddenAttribute`](https://github.com/abyssal/common/blob/master/Abyssal.Common/HiddenAttribute.cs), [`SilentAttribute`](https://github.com/abyssal/common/blob/master/Abyssal.Common/SilentAttribute.cs): Generic attributes indicating a type to be silent or hidden.
- [`LinqExtensions`](https://github.com/abyssal/common/blob/master/Abyssal.Common/Extensions/LinqExtensions.cs): A group of methods that work like language integrated queries.
- [`ServiceProviderExtensions`](https://github.com/abyssal/common/blob/master/Abyssal.Common/Extensions/ServiceProviderExtensions.cs): Utilities that allow extension and further utilization of a `System.IServiceProvider`.
- [`TypeExtensions`](https://github.com/abyssal/common/blob/master/Abyssal.Common/Extensions/TypeExtensions.cs): Shorthand methods for easily accessing information about CLR types.
  
**Scheduling**
- [`IActionScheduler`](Abyssal.Common/Scheduling/IActionScheduler.cs): A utility service that can schedule callbacks for execution at a later date. Default implementation at [`ActionScheduler`](Abyssal.Common/Scheduling/ActionScheduler.cs).
- [`ScheduledAction`](Abyssal.Common/Scheduling/ScheduledAction.cs): A data class for actions scheduled with `IActionScheduler`.

### Copyright
&copy; 2019-2021 Abyssal under the MIT License. Use at your own risk. Not responsible for your microwave exploding.
