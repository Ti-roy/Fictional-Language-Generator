# Fictional language generator
What is it?
------
Fictional language generator is a well tested API written in C# for construction strings with user defined order, length, and content, based on frequency.


What it can be used for?
------
For generating senteces, characters names, or other strings, with certain patterns.


Whats idea behind it?
------
The general idea is to give API user way of creating unique strings, by allowing them setting frequency for : 
<ul>
<li>length</li>
<li>order</li>
<li>amount of elements, this element consists of</li>
<li>element content (like string representaion or other elements)</li>
</ul>


Getting started
------
### Theoretical part
<i style="color:grey;">Warning: this naming conventions may be bad. If you know better names for program entities - contact me.</i>

API consists of 2 basic entities which are dived in root and parent variants:
<ul>
<li><b>Syntactic property</b> is entity, set to difine order of certein element types and the values syntactic unit must contain, to represent finished unit. Root variant isnt much different from parent variant (and no different at all, if you only use API). Only difference is that root property cantains lists of root syntactic units, while perent - parent syntactic units.</li>
<li><b>Syntactic unit</b> is entity, that belong to certain syntactic property. Root syntactic unit contains string representation, while parent syntactic unit - possible children and children amount.</li>
</ul>

Lets clarify this by simple example - consonant is name for root syntactic property, and this property can start from property `vowel`. It contains  root syctactic units with string representations such as `w`,`r`,`t`. Root property `vowel` can go after property `consonant`, and contains units with string representaiton `a`,`i`,`u`. Then, parent syntactic property `word`, contains parent syntactic unit, which defines that word can be created from 3 children, and this possible children are properties `vowel` and `consonant`.<br/>
<br/>
And thats all you have to undestand, to use API. In this example we created simpliest tree. with parent `word` and children `vowel` and `consonant`. Engine will produce results such as `war`, `ari` and others.

Now lets see how its done by code.

### Practical part


OK, lets jump in code, creating out last example. First of all, lets create language constructor - its an object, through all interaction with engine goes.
```csharp
LanguageConstructor languageConstructor = new LanguageConstructor();
```
Then, lets create our `consonant` property and specify order of that property:
```csharp
languageConstructor.CreateRootProperty("consonant").CanStartFrom("Start", 100).CanStartFrom("vowel", 100);
```
Ok, after creating  property `consonant`, we have to `CanStartFrom(string)` methods. `CanStartFrom("vowel", 100)` tells engine that property `consonant` can go after property `vowel` with frequency `100`. What this frequency is here for? Ok, lets say, we want that after `vowel` to go not only `consonant`, but other `vowel`, and this sequence `vowel`->`vowel` should be constructed like 10 times less often, then usual `consonant`->`vowel`. Lets write this.

```csharp
languageConstructor.CreateRootProperty("vowel").CanStartFrom("consonant", 100).CanStartFrom("vowel", 10);
```
Done.

There is still 1 unclear element. In creating property `consonant` we written `CanStartFrom("Start", 100)`. Start property is first property added to construction scheme, and we said that `consonant` should go after it. **Language construction must have properties that can go after `Start` property.**

OK, now we know how specify ordering and its order frequency, but we havent created any syntactic units, to construct our words. Lets declare some consonants.

`````csharp
languageConstructor.CreateRootSyntacticUnit("w", "consonant", 100);
languageConstructor.CreateRootSyntacticUnit("r", "consonant", 100);
languageConstructor.CreateRootSyntacticUnit("t", "consonant", 100);
`````
We declared three `consonant`s, each of them will be met with same frequency - `100`. What if we want to have letter `j` that will be met like 5 times more, than any other consonant.
`````csharp
languageConstructor.CreateRootSyntacticUnit("j", "consonant", 500);
`````
Ok, lets create some vowels:
`````csharp
languageConstructor.CreateRootSyntacticUnit("a", "vowel", 100);
languageConstructor.CreateRootSyntacticUnit("u", "vowel", 100);
languageConstructor.CreateRootSyntacticUnit("o", "vowel", 100);
`````
Here is nothing new, we declared three syntactic units of property `vowel`, wich will be met with same frequency. OK, now lets create parent property with name `word`.
`````csharp
languageConstructor.CreateParentProperty("word").CanStartFrom("Start");
`````
Difference between `CreateParentProperty(string)` and `CreateRootProperty(string)` is that parent property reference parent syntactic units, which can have as children any parent property or root property. Also, note that we again told engine that `word` can start from property `Start`. Lets create parent syntactic units and specify its children.

For creating parent syntactic unit we need to specify its children, and its children amount.
`````csharp
languageConstructor.CreateParentSyntacticUnit("word").AddPossibleChild("consonant", 100).AddPossibleChild("vowel", 80).AddChildrenAmount(3, 100);
`````
Here we told engine that syntactic unit that belong to property `word` should have children `consonant` and `vowel` (`AddPossibleChild("consonant", 100)`, `AddPossibleChild("vowel", 80)`). Child `vowel` will be chosen in 80/180 cases (total frequency 180 is sum of: `80` frequency of `vowel`, `100` frequency of `consonant`). This syntactic unit will have exatly 3 children, which is specified by method `AddChildrenAmount(3, 100)`. 

We finished creating of our basic graph. Now, only thing that left is to get results:
`````csharp
languageConstructor.GetStringListOfProprety("word", 10);
`````
And all is done. Example output listed is below:
```
juj
juj
joj
tuj
wuj
tot
jaj
wat
ruj
joj
```
Not the most incredible result, but even now, we created sequence of words that are remarkable by wast amount of letter `j` in it. Code used in this example can be found at <a href = "https://github.com/shinigamixas/Fictional-Language-Generator/blob/master/LanguageGenerator.UsageExamples/ReadMeExample.cs">this link</a>. If you want more, welcome to predefined sets section. 

API overview
------
### Property creation

#### Default properties
Currently engine contains 2 syntactic units by default. 

Its unit with property named `Start`, that represents start of new construction, made to clarify for engine, what properties follow start of construction. When setting order property can be referenced by its string name `"Start"` or: 
```csharp
BasicSyntacticUnitsSingleton.StartOfConstructionProperty
```

And unit with property name `Any`, that made to make user properties to follow any other properties. Can be referenced by its string name `"Any"` or: 
```csharp
BasicSyntacticUnitsSingleton.AnyProperty
```

### Syntactic unit creation
One main rule: **create property before its syntactic units**. You can reference not existing properties in when setting order and children by their future names, but you cant create syntactic unit, and reference it to not existing property by its string name.


Examples of usage
------
Predefined sets
------
