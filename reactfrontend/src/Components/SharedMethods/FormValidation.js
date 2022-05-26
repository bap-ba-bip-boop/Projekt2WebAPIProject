import appSettings from '../../Settings/SharedMethods/FormValidation.json'
import { formatString } from './FormatString';

export const minuteValidation = AntalMinuter =>
{
    let returnMessge = appSettings.validMessage;
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
    let returnMessge = appSettings.validMessage;
    if(!Date)
    {
        returnMessge = appSettings.errDateMissing;
    }
    return returnMessge;
}

export const projValidation = SelectedProject => {
    let returnMessge = appSettings.validMessage;
    if(SelectedProject === 0)
    {
        returnMessge = appSettings.errProjMissing;
    }
    return returnMessge;
}

export const descValidation = description =>{
    let returnMessge = appSettings.validMessage;
    if(description.length > appSettings.maxStrLength)
    {
        returnMessge = formatString(appSettings.errDescTooLong, appSettings.maxStrLength);
    }
    return returnMessge;
}