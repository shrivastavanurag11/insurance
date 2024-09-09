import { Component } from '@angular/core';
import { CanvasJSAngularChartsModule } from '@canvasjs/angular-charts';
import { HttpCommunicator } from '../HttpCommunication';
import { FormsModule } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { bar } from '../models';

@Component({
  selector: 'app-barchart',
  standalone: true,
  imports: [CanvasJSAngularChartsModule, FormsModule],
  templateUrl: './barchart.component.html',
  styleUrl: './barchart.component.css'
})
export class BarchartComponent 
{
  data!:bar[];
  message:string = '';
  title = 'angular17ssrapp';
  searchYear: string = '2021';

  constructor(private client:HttpCommunicator) {}

  
  chartOptions = {
    title: {
      text: "Number of Policies Sold Each Month"
    },
    animationEnabled: true,
    axisY: {
      includeZero: true
    },
    data: [
      {
        type: "column",
        indexLabelFontColor: "#5A5757",
        
       dataPoints: this.data.map(point => ({ x: 1, y: 1 }))
      }
    ]
  };



  ngOnInit() {
    this.fetchData();
  }

  fetchData() {
    var response = this.client.fetchData(this.searchYear);
    response.subscribe({
      next:n => {
        this.data = [...n.body!];
        let x:number = 0;
      },
      error:e => {alert(e.message);}
    });
  }
}
