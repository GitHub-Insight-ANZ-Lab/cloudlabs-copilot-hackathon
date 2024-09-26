#!/bin/bash

# Sysadmin Resource Monitoring Script

# Set the thresholds for CPU and memory usage (in percentage)
cpu_threshold=90
memory_threshold=90

# Get the current CPU usage
cpu_usage=$(top -bn1 | grep "Cpu(s)" | awk '{print $2}' | cut -d'%' -f1)

# Get the current memory usage
memory_usage=$(free | grep Mem | awk '{print $3/$2 * 100.0}' | cut -d'.' -f1)

# Check CPU usage
if [ "$cpu_usage" -gt "$cpu_threshold" ]; then
    # CPU usage exceeds the threshold, send an alert
    echo "CPU usage is above the threshold: ${cpu_usage}%"
    # Add your alerting mechanism here (e.g., send an email, log to a file, etc.)
else
    # CPU usage is within the acceptable range
    echo "CPU usage is within the acceptable range: ${cpu_usage}%"
fi

# Check memory usage
if [ "$memory_usage" -gt "$memory_threshold" ]; then
    # Memory usage exceeds the threshold, send an alert
    echo "Memory usage is above the threshold: ${memory_usage}%"
    # Add your alerting mechanism here (e.g., send an email, log to a file, etc.)
else
    # Memory usage is within the acceptable range
    echo "Memory usage is within the acceptable range: ${memory_usage}%"
fi