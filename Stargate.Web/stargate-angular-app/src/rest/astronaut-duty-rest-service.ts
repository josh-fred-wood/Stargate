import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Getpersonbyname } from '../models/getpersonbyname';
import { CreateAstronautDuty } from '../models/createastronautduty';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AstronautDutyRestService {
  constructor(private http: HttpClient) { }

  getByName(name: string): Observable<Getpersonbyname> {
    return this.http.get<Getpersonbyname>(`http://localhost:8080/astronautduty/getastronautdutiesbyname/${name}`);
  }

  createDuty(name: string, rank:string, dutyTitle: string, dutyStartDate: Date): Observable<CreateAstronautDuty> {
    const payload = {
      name: name,
      rank: rank,
      dutyTitle: dutyTitle,
      dutyStartDate: dutyStartDate
    }

    return this.http.post<CreateAstronautDuty>(`http://localhost:8080/astronautduty/createastronautduty`, payload);
  }
}
