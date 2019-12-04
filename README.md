# Logger
The goal is to create a simple logging proxy application that meets the requirements
## Requirements
* Code style
* Implement class LoggingProxy which is able to log each object method invocation which implements interface T. To fit this requirement consider inheriting LoggingProxy from dynamic object
* LoggingProxy should have public method T CreateInstance(T obj) which returns logging proxy which acts like T. To implement such a method consider using library ImpromptuInterface
* Use logger developed at task Create Logger to write logs