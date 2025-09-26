import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Getpersonbyname } from '../models/getpersonbyname';
import { Observable } from 'rxjs';
import { ObjectResponse } from '../models/objectresponse';
import { CreateResponse } from '../models/createresponse';

@Injectable({
  providedIn: 'root'
})
export class AstronautDutyRestService {
  constructor(private http: HttpClient) { }

  getByName(name: string): Observable<ObjectResponse<Getpersonbyname>> {
    return this.http.get<ObjectResponse<Getpersonbyname>>(`http://localhost:8080/astronautduty/getastronautdutiesbyname/${name}`);
  }

  createDuty(name: string, rank:string, dutyTitle: string, dutyStartDate: Date): Observable<ObjectResponse<CreateResponse>> {
    const payload = {
      name: name,
      rank: rank,
      dutyTitle: dutyTitle,
      dutyStartDate: dutyStartDate
    }

    return this.http.post<ObjectResponse<CreateResponse>>(`http://localhost:8080/astronautduty/createastronautduty`, payload);
  }
}
