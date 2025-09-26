import { Injectable } from '@angular/core';
import { PersonRestService } from '../rest/person-rest-service'

@Injectable({
  providedIn: 'root'
})
export class PersonService {
  constructor(
    private personRestService: PersonRestService
  )
}
