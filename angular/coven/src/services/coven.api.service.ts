import { Injectable } from '@angular/core';
import { Author } from 'src/app/interfaces/Author';
import { World } from 'src/app/interfaces/World';

const endpoint = "https://localhost:7127/api/"; 

@Injectable({
  providedIn: 'root'
})
export class CovenApiService {

  constructor() { }

  async GetWorldAnvilUser()
  {
    const response = await fetch(endpoint + "World/GetAnvilUser");
    const data = await response.json() as Author;
  
    console.log(data);
    return data;
  }

  async GetWorldInfo(): Promise<World> {
    const response = await fetch(endpoint + "World/GetWorldInfo");
    const data = await response.json() as World;
  
    console.log(data);
    return data;
  }
}
