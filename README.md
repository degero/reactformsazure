# React forms with azure

## Overview

Demonstration of react forms (formik) with an option of AZ functions (http trigger) or Web api (.net core 3.1) writing to AZ Storage table

## Dev Dependencies

- yarn
- vscode
- azure storage emulator
- azure storage explorer

## Getting started

## Architecture

API 1: Azure Function App (httptrigger for form storage)
Azure Storage (for function app)
DB: Azure Cosmos Db (for api / http function storage of form data)
Azre App Service
API 2: for .net core webapi
Website: react webapp
