const validationSettings = require('../../Settings/SharedMethods/FormValidation.json');
const { formatString } = require('./FormatString');


const { minuteValidation, dateValidation, projValidation, descValidation } = require('./FormValidation')

const createOfLength = length => 
{
    let message = "";
    for(let i = 0; i < length; i++)
    {
        message += "a";
    }
    return message;
}
const getrandomInteger = (min,max) =>
    Math.floor(
        Math.random() * (max - min + 1) + min
    );

//MinuteValidation Tests
test(
    "When input does not exist should return appropriate message",
    () => {
        const input = undefined;
        expect(
            minuteValidation(input)
        )
        .toBe(
            validationSettings.errMissingMinutes
        )
    }
);
test(
    `When input is less than ${validationSettings.minMinutes} should return appropriate message`,
    () => {
        const input = validationSettings.minMinutes - 1;
        expect(
            minuteValidation(input)
        )
        .toBe(
            formatString(validationSettings.errLessThanAllowedMin, validationSettings.minMinutes)
        )
    }
);
test(
    `When input is more than ${validationSettings.maxMinutes} should return appropriate message`,
    () => {
        const input = validationSettings.maxMinutes + 1;
        expect(
            minuteValidation(input)
        )
        .toBe(
            formatString(validationSettings.errMoreThanAllowedMax, validationSettings.maxMinutes)
        )
    }
);
test(
    "When input is correct should return appropriate message",
    () => {
        const input = Math.floor(
            Math.random() * (validationSettings.maxMinutes - validationSettings.minMinutes + 1) + validationSettings.minMinutes
        )
        expect(
            minuteValidation(input)
        )
        .toBe(
            validationSettings.validMessage
        )
    }
);

//DateValidation Tests
test(
    "When Date does not exist should return appropriate message",
    () => {
        const input = "";
        expect(
            dateValidation(input)
        )
        .toBe(
            validationSettings.errDateMissing
        )
    }
)
test(
    "When Date exists should return appropriate message",
    () => {
        const input = Date.now;
        expect(
            dateValidation(input)
        )
        .toBe(
            validationSettings.validMessage
        )
    }
)

//ProjValidation Tests
test(
    `When ProjectID equals ${validationSettings.forbiddenProjId} should return appropriate message`,
    () => {
        const input = validationSettings.forbiddenProjId;
        expect(
            projValidation(input)
        )
        .toBe(
            validationSettings.errProjMissing
        )
    }
)
test(
    `When ProjectID does not equal ${0} should return appropriate message`,
    () => {
        const min = 1;
        const max = 10;
        const input = getrandomInteger(min, max);
        expect(
            dateValidation(input)
        )
        .toBe(
            validationSettings.validMessage
        )
    }
)

//DescValidation Tests
test(
    `When Desc is longer than ${validationSettings.maxStrLength} should return appropriate message`,
    () => {
        const length = validationSettings.maxStrLength + 1;
        const input = createOfLength(length);
        expect(
            descValidation(input)
        )
        .toBe(
            formatString(validationSettings.errDescTooLong, validationSettings.maxStrLength)
        )
    }
)
test(
    `When Desc is shorter or equal to ${validationSettings.maxStrLength} should return appropriate message`,
    () => {
        const min = 0;
        const max = validationSettings.maxStrLength;
        const length = getrandomInteger(min, max);
        const input = createOfLength(length);
        expect(
            descValidation(input)
        )
        .toBe(
            validationSettings.validMessage
        )
    }
)