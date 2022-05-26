
export const formatString= (stringToChange, ...args) => {
    let newString = stringToChange;
    for(let i = 1; i <= args.length; i++)
    {
        newString = newString.split("{"+i+"}").join(`${args[i-1]}`);
    }

    return newString;
}