# Fictional language generator
What is it?
------
Fictional language generator is a well tested API for construction strings with user defined order, length, and content, based on frequency.


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


API overview
------
<sub><i>Warning: this naming conventions may be bad. If you know better names for program entities - contact me.</i></sub> <br/>
API consists of 2 basic entities which are dived in root and parent variants:
<ul>
<li>Syntactic property</li>
<li>Syntactic unit</li>
</ul>
<b>Syntactic property</b> is entity, set to difine order of certein element types and the values syntactic unit must contain, to represent finished unit. Root variant isnt much different from parent variant (and no different at all, if you only use API). Only difference is that root property cantains lists of root syntactic units, while perent - parent syntactic units.<br/>
<b>Syntactic unit</b> is entity, that belong to certain syntactic property. Root syntactic unit contains string representation, while parent syntactic unit - possible children and children amount.

Lets clarify this by simple example - "consonant" is name for root syntactic property, and this property can start from property "vowel". It contains  root syctactic units with string representations such as *"w","r","t",* and others. Root property "vowel" can go after property "consonants", and contains units with string representaiton *"a","i","u",* and others. Then, parent syntactic property "word", contains parent syntactic unit, which defines that word can be created from 3 children, and this possible children are properties "vowel" and "consonant".

And thats all you have to undestand, to use API. In this example we created simpliest tree. with parent "word" and children "vowel" and "consonant". Engine will produce results such as *"war", "ari"* and others.

Now lets see how its done by code.

Examples of usage
------
Predefined sets
------
