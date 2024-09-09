import { Component } from '@angular/core';
import { CanvasJSAngularChartsModule } from '@canvasjs/angular-charts';
import { HttpCommunicator } from '../HttpCommunication';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-barchart',
  standalone: true,
  imports: [CanvasJSAngularChartsModule, FormsModule],
  templateUrl: './barchart.component.html',
  styleUrl: './barchart.component.css'
})
export class BarchartComponent 
{
  title = 'angular17ssrapp';
	// chartOptions = {
	// 	title: {
	// 		text: "Angular Column Chart with Index Labels"
	// 	},
	// 	animationEnabled: true,
	// 	axisY: {
	// 		includeZero: true
	// 	},
	// 	data:
  //   [
  //     {
	// 		type: "column", //change type to bar, line, area, pie, etc
	// 		//indexLabel: "{y}", //Shows y value on all Data Points
	// 		indexLabelFontColor: "#5A5757",
	// 		dataPoints: [
	// 			{ x: 10, y: 71 },
	// 			{ x: 20, y: 55 },
	// 			{ x: 30, y: 50 },
	// 			{ x: 40, y: 65 },
	// 			{ x: 50, y: 71 },
	// 			{ x: 60, y: 92, indexLabel: "Highest\u2191" },
	// 			{ x: 70, y: 68 },
	// 			{ x: 80, y: 38, indexLabel: "Lowest\u2193"  },
	// 			{ x: 90, y: 54 },
	// 			{ x: 100, y: 60 }
	// 		]
	// 	}]
	// }
  searchYear: string = '';
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
        dataPoints: []
      }
    ]
  };

  constructor(private http: HttpCommunicator) {}

  ngOnInit() {
    this.fetchData();
  }

  fetchData() {
    const endpoint = this.searchYear ? `YOUR_API_ENDPOINT?year=${this.searchYear}` : 'YOUR_API_ENDPOINT';
    
  }

}
