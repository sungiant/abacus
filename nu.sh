#!/bin/bash

set -x #echo on

rm -rf packages/

nuget install -o packages/
