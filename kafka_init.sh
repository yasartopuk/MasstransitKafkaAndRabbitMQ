#!/bin/bash

# Kafka server'ın tam olarak başlamasını bekleyin
sleep 20

# Topic'leri oluşturun
kafka-topics.sh --create --bootstrap-server kafka:9092 --replication-factor 1 --partitions 1 --topic log-topic
kafka-topics.sh --create --bootstrap-server kafka:9092 --replication-factor 1 --partitions 1 --topic notification-topic

# Kafka server'ı başlatın (Eğer bu script Kafka'yı başlatmıyorsa, bu satırı kaldırın)
# /etc/confluent/docker/run
