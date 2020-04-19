# BehaviourTree

[![Build status](https://ci.appveyor.com/api/projects/status/ad6prnywckev6s4b?svg=true)](https://ci.appveyor.com/api/projects/status/ad6prnywckev6s4b?svg=true)


## Installation
 
```
Install-Package BehaviourTree
```

## Demo
https://www.youtube.com/watch?v=OeVo2l-O0vU

[![IMAGE ALT TEXT HERE](https://img.youtube.com/vi/OeVo2l-O0vU/0.jpg)](https://www.youtube.com/watch?v=OeVo2l-O0vU)

## Features

 - Generic context 
 - Extensible
 - Fluent Builder
 - Basic node types included
 - Tree visualizer (Coming soon)

## Usage (FluentBuilder)

``` cs    
var behaviourTree = FluentBuilder.Create<MyContext>()
    .Sequence("root")
        .Do("walk to door", WalkToDoorFunc)
        .Selector("open door sequence")
            .Do("open door", OpenDoorFunc)
            .Sequence("locked door sequence")
                .Do("unlock door", UnlockDoorFunc)
                .Do("open door", OpenDoorFunc)
            .End()
            .Do("smash door", SmashDoorFunc)
        .End()
        .Do("walk through door", WalkThroughDoorFunc)
        .Do("close door", CloseDoorFunc)
    .End()
    .Build();
```

## Node Types

### Leaves

#### Action
``` cs    
builder.Do("my-action", context => BehaviourStatus.Succeeded)
```

#### Wait
``` cs    
builder.Wait("my-wait", 3000) // 3 seconds
```

#### Condition
``` cs    
builder.Condition("my-condition", context => true)
```

### Composites

#### Sequence
``` cs    
builder.Sequence("my-sequence")
    .Do("action1", context => BehaviourStatus.Succeeded)
    .Do("action2", context => BehaviourStatus.Succeeded)
    .Do("action3", context => BehaviourStatus.Succeeded)
    ...
.End()
```

#### Selector
``` cs    
builder.Selector("my-selector")
    .Do("action1", context => BehaviourStatus.Failed)
    .Do("action2", context => BehaviourStatus.Succeeded)
    .Do("action3", context => BehaviourStatus.Succeeded)
    ...
.End()
```

#### RandomSequence
``` cs    
builder.RandomSequence("my-random-sequence")
    .Do("action1", context => BehaviourStatus.Succeeded)
    .Do("action2", context => BehaviourStatus.Succeeded)
    .Do("action3", context => BehaviourStatus.Succeeded)
    ...
.End()
```

#### RandomSelector
``` cs    
builder.RandomSelector("my-random-selector")
    .Do("action1", context => BehaviourStatus.Failed)
    .Do("action2", context => BehaviourStatus.Succeeded)
    .Do("action3", context => BehaviourStatus.Succeeded)
    ...
.End()
```

#### PrioritySequence
``` cs    
builder.PrioritySequence("my-priority-sequence")
    .Do("action1", context => BehaviourStatus.Succeeded)
    .Do("action2", context => BehaviourStatus.Succeeded)
    .Do("action3", context => BehaviourStatus.Succeeded)
    ...
.End()
```

#### PrioritySelector
``` cs    
builder.PrioritySelector("my-priority-selector")
    .Do("action1", context => BehaviourStatus.Failed)
    .Do("action2", context => BehaviourStatus.Succeeded)
    .Do("action3", context => BehaviourStatus.Succeeded)
    ...
.End()
```

#### SimpleParallel
``` cs
public enum SimpleParallelPolicy
{
    BothMustSucceed,
    OnlyOneMustSucceed
}

var policy = SimpleParallelPolicy.BothMustSucceed;

builder.SimpleParallel("my-parallel", policy)
    .Do("action1", context => BehaviourStatus.Running)
    .Do("action2", context => BehaviourStatus.Running)
.End()
```

### Decorators

#### Cooldown
``` cs    
builder.Cooldown("my-cooldown", 4000) // 4 seconds
    .Do("action1", context => BehaviourStatus.Failed)
.End()
```

#### Failer
``` cs    
builder.AlwaysFail("my-failer")
    .Do("action1", context => BehaviourStatus.Succeeded)
.End()
```

#### Succeeder
``` cs    
builder.AlwaysSucceed("my-succeeder")
    .Do("action1", context => BehaviourStatus.Failed)
.End()
```

#### Inverter
``` cs    
builder.Invert("my-inverter")
    .Do("action1", context => BehaviourStatus.Failed)
.End()
```

#### RateLimiter (Cache)
``` cs    
builder.LimitCallRate("my-rate-limiter", 1000) // 1 second
    .Do("action1", context => BehaviourStatus.Failed)
.End()
```

#### Repeat
``` cs    
builder.Repeat("my-repeater", 5)
    .Do("action1", context => BehaviourStatus.Failed)
.End()
```

#### TimeLimit
``` cs    
builder.TimeLimit("my-time-limit", 5000) // has 5 seconds to complete or will fail
    .Do("action1", context => BehaviourStatus.Running)
.End()
```

#### UntilSuccess
``` cs    
builder.UntilSuccess("my-until-success")
    .Do("action1", context => BehaviourStatus.Failed)
.End()
```

#### UntilFailed
``` cs    
builder.UntilFailed("my-until-failed")
    .Do("action1", context => BehaviourStatus.Succeeded)
.End()
```

#### Random
``` cs    
builder.Random("my-random", 0.6) // will call child 60% of the time
    .Do("action1", context => BehaviourStatus.Succeeded)
.End()
```

#### SubTree
``` cs    
builder.SubTree("my-sub-tree", otherBehaviourTree)
```

