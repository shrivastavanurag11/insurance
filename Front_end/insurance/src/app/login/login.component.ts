import { HttpClientModule, provideHttpClient, withFetch } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl } from '@angular/forms';
import { HttpCommunicator } from '../HttpCommunication';
import { DataTransferModel, SecurityTokenModel } from '../models';
import { routes } from '../app.routes';
import { RouterLink, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterOutlet,RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  providers:[]
})
export class LoginComponent {
  message:string='';

  constructor(private client:HttpCommunicator){}

    login(username:string , password:string):void{
      this.message='';
      var response = this.client.Login(username,password);

      response.subscribe({
        error:e => {this.message = e.message},
        next:n=>
        { 
          var model=<DataTransferModel>n.body;
          if(model.success==false)
            {
            this.message = model.message!;
          }
          else
          {
            let m = <SecurityTokenModel>model.data;
            sessionStorage.setItem('jwttoken',m.jwttoken);
            sessionStorage.setItem('role', m.role);
            this.message="Login Successful"
          }
        }
      });

    }
}
