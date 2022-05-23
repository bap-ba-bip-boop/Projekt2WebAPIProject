import appSettings from '../../Settings/SharedMethods/FormValidation.json'

const formatString= (stringToChange, ...args) => {
    let newString = stringToChange;
    console.log(stringToChange, args);
    for(let i = 1; i <= args.length; i++)
    {
        newString = newString.split("{"+i+"}").join(`${args[i-1]}`);
    }

    return newString;
}

export const minuteValidation = AntalMinuter =>
{
    let returnMessge = "";
    if(!AntalMinuter || AntalMinuter === 0)
    {
      returnMessge = appSettings.errMissingMinutes;
    }
    else if(AntalMinuter < appSettings.minMinutes)
    {
        returnMessge = formatString(appSettings.errLessThanAllowedMin, appSettings.minMinutes)
    }
    else if(AntalMinuter > appSettings.maxMinutes)
    {
        returnMessge = formatString(appSettings.errMoreThanAllowedMax, appSettings.maxMinutes);
    }
    return returnMessge;
} 

export const dateValidation = Date => {
    let returnMessge = "";
    if(!Date)
    {
        returnMessge = appSettings.errDateMissing;
    }
    return returnMessge;
}

export const projValidation = SelectedProject => {
    let returnMessge = "";
    if(SelectedProject === 0)
    {
        returnMessge = appSettings.errProjMissing;
    }
    return returnMessge;
}

export const descValidation = description =>{
    let returnMessge = "";
    if(description.length > appSettings.maxStrLength)
    {
        returnMessge = formatString(appSettings.errDescTooLong, appSettings.maxStrLength);
    }
    return returnMessge;
}