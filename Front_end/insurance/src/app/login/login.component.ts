import { HttpClientModule, provideHttpClient, withFetch } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl } from '@angular/forms';
import { HttpCommunicator } from '../HttpCommunication';
import { SecurityTokenModel } from '../models';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  providers:[]
})
export class LoginComponent {
  message:string='';

  constructor(private client:HttpCommunicator){}

    login(username:string , password:string){
      this.message='';
      var response = this.client.Login(username,password);

      response.subscribe({
        error:e => {this.message = e.message},
        next:n=>
        {if(n.body?.Data==null)
          {
            this.message = n.body?.['Message']!;
             var t=0;
          }
          else
          {
            let model = <SecurityTokenModel>n.body?.Data;
            sessionStorage.setItem('jwttoken',model.jwttoken);
            sessionStorage.setItem('role', model.role);
          }
        }
      });
     // return false;
    }
}
