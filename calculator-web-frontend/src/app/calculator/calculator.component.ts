import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-calculator',
  templateUrl: './calculator.component.html',
  styleUrls: ['./calculator.component.css']
})
export class CalculatorComponent implements OnInit {

  calculator: ICalculator  = {
    numberOne: 0,
    numberTwo: 0,
    result: 0,
  }

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }

  Add(numberOne: number, numberTwo: number): number {

    this.http.get<number>(`https://localhost:7276/Add/${numberOne}/${numberTwo}`)
      .subscribe(result => this.calculator.result = result);

    return this.calculator.result;
  }

  Subtract(numberOne: number, numberTwo: number): number {

    this.http.get<number>(`https://localhost:7276/Subtract/${numberOne}/${numberTwo}`)
      .subscribe(result => this.calculator.result = result);

    return this.calculator.result;
  }

  Multiply(numberOne: number, numberTwo: number): number {

    this.http.get<number>(`https://localhost:7276/Multiply/${numberOne}/${numberTwo}`)
      .subscribe(result => this.calculator.result = result);

    return this.calculator.result;
  }

  Divide(numberOne: number, numberTwo: number): number {

    this.http.get<number>(`https://localhost:7276/Divide/${numberOne}/${numberTwo}`)
      .subscribe(result => this.calculator.result = result);

    return this.calculator.result;
  }
}

interface ICalculator {
  numberOne: number;
  numberTwo: number;
  result: number;
}
