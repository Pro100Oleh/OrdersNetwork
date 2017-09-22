# OrdersNetwork

## About
This is C# demo with my blockchain implementation

## Requirements
* A distributed blockchain network to allow communicate 3 different types of users
  * Order makers: user publishes an order for execution. Sends to network a message about new order with Payment property.
  * Order executors: users that listen for new orders on network and peek up some of them for execution. Sends to network a message about intention to execute list of desired orders and Fee.
  * Network nodes that support this blockchain. Responsible to keep orders and order assignments in consistent state.
* Blockchain should support different kinds of orders – it should be pluggable.
* Demo contains plugin to support WalkDog orders that describe a walk dog task with properties like dog info, _StartTime_ and _Location_.
