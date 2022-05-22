import appSettings from '../../Settings/SharedMethods/FormValidation.json'
import { format } from 'react-string-format';

export const minuteValidation = AntalMinuter =>
{
    
    console.log(appSettings);
    let returnMessge = "";
    if(!AntalMinuter || AntalMinuter === 0)
    {
      returnMessge = appSettings.errMissingMinutes;
    }
    else if(AntalMinuter < appSettings.minMinutes)
    {
        //console.log(appSettings.errLessThanAllowedMin.format(appSettings.minMinutes))
        returnMessge = format(appSettings.errLessThanAllowedMin, appSettings.minMinutes);
        //console.log("returnMEssage: " + returnMessge);
    }
    else if(AntalMinuter > appSettings.maxMinutes)
    {
        returnMessge = appSettings.errMoreThanAllowedMax.format(appSettings.maxMinutes);
    }
    return returnMessge;
} 