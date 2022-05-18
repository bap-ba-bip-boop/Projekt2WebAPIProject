const url = 'https://localhost:7045/tidsregistrering';

export const fetchRegs = async () => {
  const response = await fetch(url)
  const json = await response.json()

  return  json
}