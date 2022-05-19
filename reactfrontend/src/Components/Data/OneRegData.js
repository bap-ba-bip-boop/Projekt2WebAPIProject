const url = 'https://localhost:7045/tidsregistrering';

export const fetchOneReg = async id => {
  const response = await fetch(url+`/${id}`)
  const json = await response.json()

  return  json
}