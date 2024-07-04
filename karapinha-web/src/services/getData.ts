interface getAllDataProps {
    url : string;
}

export async function getAllData({url}:getAllDataProps){
    try {
        const response = await fetch(
        `${url}`);
        if (response.ok) {
          const data = await response.json();
          console.log(data);
          return data;
        } else {
          console.error("Failed to fetch data");
        }
      } catch (error) {
        console.error("Error fetching data:", error);
      }
}