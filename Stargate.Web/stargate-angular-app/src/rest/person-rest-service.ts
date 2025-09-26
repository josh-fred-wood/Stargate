import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GetPeopleResult } from '../models/getpeopleresult';
import { Getpersonbyname } from '../models/getpersonbyname';
import { ObjectResponse } from '../models/objectresponse';
import { CreateResponse } from '../models/createresponse';

@Injectable({
  providedIn: 'root'
})
export class PersonRestService {
  constructor(private http: HttpClient) { }

  getPeople(): Observable<ObjectResponse<GetPeopleResult>> {
    return this.http.get<ObjectResponse<GetPeopleResult>>(`http://localhost:8080/person/getpeople`)
  }

  getByName(name: string): Observable<ObjectResponse<Getpersonbyname>> {
    return this.http.get<ObjectResponse<Getpersonbyname>>(`http://localhost:8080/person/getpeople/${name}`)
  }

  create(name: string): Observable<ObjectResponse<CreateResponse>> {
    const payload = {
      name: name
    }
    return this.http.post<ObjectResponse<CreateResponse>>(`http://localhost:8080/person`, payload);
  }
}
