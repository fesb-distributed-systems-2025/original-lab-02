
This repository contains examples for Lab 02: Synchronization.

## Problem

The general problem we are trying to solve is next:

We have a function called `DoCalculation()` which increments a shared variable `Sum` `NumberOfIterations` times. For example,
if `NumberOfIterations = 1000` after one call to `DoCalculation()` the `Sum` will have a value of 1000 and after 2 calls it will have `2000` and so on.

We are trying to optimize our program to do this in `NumberOfTasks` parallel threads. For example, if `NumberOfTasks = 8` and `NumberOfIterations = 1000` we will have
8 parallel threads incrementing the `Sum` variable. Each thread will increment the `Sum` by 1000 so we expect the value of 8*1000=8000.

## Contents

1. Syncronization.Simple.Unprotected

This example showcases the soultion where there are no synchronization mechanisms between threads. We expect the value of 8000
but most of the time we will get invalid value and each time a different one. Try to understand what is the problem here.

2. Syncronization.Simple.AtomicOperation

This example solves the problem by using the `atomic` variable for sum. This is only useful for scenarios where only one variable must be protected.

3. Syncronization.Simple.Lock

This example solves the problem by locking the part of software in which the sum is incremented so only ine thread at a time can access it.

4. Syncronization.Simple.SpinLock

This example solves the problem by implementing a mechanism similar to the `lock` but only using `atomic` (`Interlocked`) variables.

5. Syncronization.Simple.Deadlock

An example of a problem which occurs when improperly using multiple `lock`s.

6. Syncronization.Simple.AllowMultipleThreads

An example of counting semaphores.