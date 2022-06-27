import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-calculator',
  templateUrl: './calculator.component.html',
  styleUrls: ['./calculator.component.css']
})
export class CalculatorComponent implements OnInit {

  private readonly mCalculatorConnectionString : string = "https://localhost:7276/Calculator";

  calculator: ICalculator  = {
    numberOne: 0,
    numberTwo: 0,
    result: 0,
  }

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }

  Add(numberOne: number, numberTwo: number): number {

    this.http.get<number>(`${this.mCalculatorConnectionString}/Add/${numberOne}/${numberTwo}`)
      .subscribe(result => this.calculator.result = result);

    return this.calculator.result;
  }

  Subtract(numberOne: number, numberTwo: number): number {

    this.http.get<number>(`${this.mCalculatorConnectionString}/Subtract/${numberOne}/${numberTwo}`)
      .subscribe(result => this.calculator.result = result);

    return this.calculator.result;
  }

  Multiply(numberOne: number, numberTwo: number): number {

    this.http.get<number>(`${this.mCalculatorConnectionString}/Multiply/${numberOne}/${numberTwo}`)
      .subscribe(result => this.calculator.result = result);

    return this.calculator.result;
  }

  Divide(numberOne: number, numberTwo: number): number {

    this.http.get<number>(`${this.mCalculatorConnectionString}/Divide/${numberOne}/${numberTwo}`)
      .subscribe(result => this.calculator.result = result);

    return this.calculator.result;
  }
}

interface ICalculator {
  numberOne: number;
  numberTwo: number;
  result: number;
}
