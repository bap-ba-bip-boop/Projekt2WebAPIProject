const { formatString } = require('./FormatString');

const stringToBeFormated = "Hello my {1} is {2}. What is your {1}";
const value1 = "name";
const value2 = "John";

const expectedAnswer = "Hello my name is John. What is your name";

test(
    `Testintg the ${formatString.name} method`,
    () =>{
        expect(
            formatString(stringToBeFormated, value1, value2)
        )
        .toBe(
            expectedAnswer
        )
    }
);