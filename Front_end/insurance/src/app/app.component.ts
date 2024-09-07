import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, FormsModule, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'insurance';
  username:string | boolean  = false;
  

  


// 
  constructor(private router:Router)
  {
    try{
    let decodedToken: any | null = jwt_decode(<string>sessionStorage.getItem('jwttoken'));
    if(decodedToken != null)
    this.username =  decodedToken.Username; // Adjust this based on your token's structure
    }
    catch(error){}
  }


  logout():void
{
  sessionStorage.removeItem('jwttoken');
  this.router.navigate(['login']);
}

}
function jwt_decode(token: string): any {
  throw new Error('Function not implemented.');
}

