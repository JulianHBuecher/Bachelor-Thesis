FROM nginx:latest

COPY ./conf.d/sites-available/jb-thesis-project.conf /etc/nginx/conf.d/jb-thesis-project.conf
COPY ./conf.d/sites-available/redirect.conf /etc/nginx/conf.d/redirect.conf

COPY ./reverse_proxy.certificates/cert.pem /etc/ssl/private/cert.pem
COPY ./reverse_proxy.certificates/key.pem /etc/ssl/private/key.pem

COPY ./basic.conf/nginx.conf /etc/nginx/nginx.conf
COPY ./basic.conf/common.conf /etc/nginx/common.conf
COPY ./basic.conf/common_location.conf /etc/nginx/common_location.conf
COPY ./basic.conf/dhparams.pem /etc/nginx/dhparams.pem
COPY ./basic.conf/ssl.conf /etc/nginx/ssl.conf

COPY ./ClientCerts/ca.crt /etc/nginx/client_certs/ca.crt
