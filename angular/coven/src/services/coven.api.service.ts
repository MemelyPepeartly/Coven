import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import fetch from 'node-fetch';

const endpoint = "https://localhost:7127/api/"; 

@Injectable({
  providedIn: 'root'
})
export class CovenApiService {

  constructor() { }

  

  async GetWorldAnvilUser()
  {
    const response = await fetch(endpoint + "World/GetAnvilUser");
    const data = await response.json();

    console.log(data);
  }
}
