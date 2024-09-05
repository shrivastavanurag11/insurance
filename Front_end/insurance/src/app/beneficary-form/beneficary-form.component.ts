import { NgFor } from '@angular/common';
import { Component, Input } from '@angular/core';
import { FormBuilder, FormsModule, NgControl, NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-beneficary-form',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './beneficary-form.component.html',
  styleUrl: './beneficary-form.component.css'
})
export class BeneficaryFormComponent {
  policyid!:number;
  @Input() data: any;

  constructor(private act:ActivatedRoute){}

  ngOnInit(){
    this.act.params.subscribe(x => {this.policyid = x['id']});
  }
  onSubmit(){}
}
