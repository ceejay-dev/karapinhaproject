interface getAllServicosProps {
    url : string;
}

export async function getAllServicos({url}:getAllServicosProps){
    try {
        const response = await fetch(
        `${url}`);
        if (response.ok) {
          const data = await response.json();
          console.log(data);
          return data;
        } else {
          console.error("Failed to fetch serviços");
        }
      } catch (error) {
        console.error("Error fetching serviços:", error);
      }
}