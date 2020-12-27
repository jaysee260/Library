(
    source ./.env
    docker exec -it $POSTGRES_CONTAINER_NAME psql postgresql://$POSTGRES_USER@localhost:$POSTGRES_PORT/$POSTGRES_DB
)