import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'AngularApi';
  users : any;

  constructor(private http : HttpClient){

  }
  ngOnInit() {

   this.Getusers();
    
  }

  Getusers()
  {
    this.http.get('https://localhost:44333/api/users').subscribe(Response =>
    {this.users = Response;},
    error => {
      console.log(error);
    })
  }
}

